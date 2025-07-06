import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; // ← вот это
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
interface ExamDto {
  id?: number;
  examDate: string;
  examScore: number;
  studentNumber: number;
  lessonCode: string;
  validationErrors?: { [key: string]: string[] };
}

@Component({
  selector: 'app-exam-form',
  templateUrl: './exams-form.component.html',
  styleUrls: ['./exams-form.component.css'],
  standalone: true,
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    CommonModule,
    MatDialogModule,
 
  ]
})
export class ExamFormComponent implements OnInit {
  exam: ExamDto;
  students: any[] = [];
  lessons: any[] = [];

  constructor(
    public dialogRef: MatDialogRef<ExamFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private http: HttpClient
  ) {
    this.exam = {
      id: data?.id ?? undefined,
      examDate: data?.examDate ?? '',
      examScore: data?.examScore ?? 0,
      studentNumber: data?.studentNumber ?? 0,
      lessonCode: data?.lessonCode ?? '',
      validationErrors: {}
    };
  }

//   ngOnInit(): void {
// console.log('exam.studentNumber:', this.exam.studentNumber); // 👈 это важно
//   console.log('exam.lessonCode:', this.exam.lessonCode);

//   if (this.data?.students) {
//     this.students = this.data.students;
//   } else {
//     this.http.get<any[]>('https://localhost:7066/api/v1/students').subscribe(data => {
//       this.students = data;
//     });
//   }

//   if (this.data?.lessons) {
//     this.lessons = this.data.lessons;
//   } else {
//     this.http.get<any[]>('https://localhost:7066/api/v1/lessons').subscribe(data => {
//       this.lessons = data;
//     });
//   }
// }
ngOnInit(): void {
  if (this.data?.students) {
    this.students = this.data.students;
  } else {
    this.http.get<any[]>('https://localhost:7066/api/v1/students').subscribe(data => {
      this.students = data;
      console.log('Students data:', this.students); // Добавьте для отладки

      // ✅ Установить первый доступный OrderNumber
      if (!this.exam.studentNumber && this.students.length > 0) {
        this.exam.studentNumber = this.students[0].OrderNumber;
      }
    });
  }

  if (this.data?.lessons) {
    this.lessons = this.data.lessons;
  } else {
    this.http.get<any[]>('https://localhost:7066/api/v1/lessons').subscribe(data => {
      this.lessons = data;
      console.log('Lessons data:', this.lessons); // Добавьте для отладки

      // ✅ Установить первый доступный Code
      if (!this.exam.lessonCode && this.lessons.length > 0) {
        this.exam.lessonCode = this.lessons[0].Code;
      }
    });
  }
}
  save(): void {
    const body = {
      Id: this.exam.id,
      ExamDate: this.exam.examDate,
      ExamScore: this.exam.examScore,
      StudentNumber: this.exam.studentNumber,
      LessonCode: this.exam.lessonCode
    };

    const request = this.exam.id
      ? this.http.put('https://localhost:7066/api/v1/exams', body)
      : this.http.post('https://localhost:7066/api/v1/exams', { ...body, Id: undefined });

    request.subscribe({
      next: () => this.dialogRef.close(true),
      error: err => {
        if (err.status === 400 && err.error?.errors) {
          this.exam.validationErrors = err.error.errors;
        } else {
          alert('Xəta baş verdi. Zəhmət olmasa yenidən cəhd edin');
        }
      }
    });
  }

  cancel(): void {
    this.dialogRef.close(null);
  }
}
