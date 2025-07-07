// import { Component, Inject, OnInit } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
// import { MatFormFieldModule } from '@angular/material/form-field';
// import { MatInputModule } from '@angular/material/input';
// import { MatSelectModule } from '@angular/material/select';
// import { MatButtonModule } from '@angular/material/button';
// import { HttpClient } from '@angular/common/http';
// import { MatDialogModule } from '@angular/material/dialog';
// import { DxSelectBoxModule } from 'devextreme-angular';

// interface ExamDto {
//   id?: number;
//   examDate: string;
//   examScore: number;
//   studentNumber: number;
//   lessonCode: string;
//   validationErrors?: { [key: string]: string[] };
// }

// @Component({
//   selector: 'app-exam-form',
//   templateUrl: './exams-form.component.html',
//   styleUrls: ['./exams-form.component.css'],
//   standalone: true,
//   imports: [
//     FormsModule,
//     MatFormFieldModule,
//     MatInputModule,
//     MatSelectModule,
//     MatButtonModule,
//     CommonModule,
//     MatDialogModule,
//     DxSelectBoxModule
//   ],
// })


// export class ExamFormComponent implements OnInit {
//   exam: ExamDto;
//   students: any[] = [];
//   lessons: any[] = [];
// private formatDateString(dateStr: string | Date): string {
//   const date = new Date(dateStr);
//   const day = String(date.getDate()).padStart(2, '0');
//   const month = String(date.getMonth() + 1).padStart(2, '0');
//   const year = date.getFullYear();
//   return `${day}/${month}/${year}`;
// }
// isArray(value: any): boolean {
//     return Array.isArray(value);
//   }

// private toDateInputValue(date: string | Date): string {
//   const d = new Date(date);
//   const year = d.getFullYear();
//   const month = ('0' + (d.getMonth() + 1)).slice(-2);
//   const day = ('0' + d.getDate()).slice(-2);
//   return `${year}-${month}-${day}`;
// }


//   constructor(
//     public dialogRef: MatDialogRef<ExamFormComponent>,
//     @Inject(MAT_DIALOG_DATA) public data: any,
//     private http: HttpClient
//   ) {
//       console.log('Recieved exam data:', data);

//       const formattedDate = data?.ExamDate 
//       ? this.toDateInputValue(data.ExamDate) : '';


//     this.exam = {
//       id: data?.Id ?? undefined,
//       examDate: formattedDate ?? '',
//       examScore: data?.ExamScore ?? 0,
//       studentNumber: data?.StudentNumber ?? 0,
//       lessonCode: data?.LessonCode ?? '',
//       validationErrors: {}
//     };
//   }


// ngOnInit(): void {
//   if (this.data?.students) {
//     this.students = this.data.students;
//   } else {
//     this.http.get<any[]>('https://localhost:7066/api/v1/students').subscribe(data => {
//       this.students = data;
//       console.log('Students data:', this.students); 

     
//       if (!this.exam.studentNumber && this.students.length > 0) {
//         this.exam.studentNumber = this.students[0].OrderNumber;
//       }
//     });
//   }

//   if (this.data?.lessons) {
//     this.lessons = this.data.lessons;
//   } else {
//     this.http.get<any[]>('https://localhost:7066/api/v1/lessons').subscribe(data => {
//       this.lessons = data;
//           if (!this.exam.lessonCode && this.lessons.length > 0) {
//         this.exam.lessonCode = this.lessons[0].Code;
//       }
//     });
//   }
// }



// save(): void {
//   this.exam.validationErrors = {};

//   let parsedDate: string | null = null;

 
//   if (this.exam.examDate?.trim()) {
//     const dateObj = new Date(this.exam.examDate);
//     parsedDate = dateObj.toISOString(); 
//   }

//   const body = {
//     Id: this.exam.id,
//     ExamDate: parsedDate,
//     ExamScore: this.exam.examScore,
//     StudentNumber: this.exam.studentNumber,
//     LessonCode: this.exam.lessonCode
//   };

  

//   const request = this.exam.id
//     ? this.http.put('https://localhost:7066/api/v1/exams', body)
//     : this.http.post('https://localhost:7066/api/v1/exams', { ...body, Id: undefined });

//   request.subscribe({
//     next: () => this.dialogRef.close(true),
//     error: err => {
//       const errors = err.error?.errors;
//       if (err.status === 400 && errors) {
//         this.exam.validationErrors = { ...errors };
        
//       } else if (err.error?.message) {
//         this.exam.validationErrors = {
//           General: [err.error.message]
//         };
//       } else {
//         this.exam.validationErrors = {
//           General: ['Xəta baş verdi. Zəhmət olmasa yenidən cəhd edin']
//         };
//       }
//     }
//   });
// }
// public validateScore(event: Event): void {
//   const input = event.target as HTMLInputElement;
//   let value = parseInt(input.value, 10);

//   if (isNaN(value)) {
//     value = 0;
//   }

//   if (value > 700) {
//     input.value = '700';
//     this.exam.examScore = 700;

    
//     if (this.exam.validationErrors?.['ExamScore']) {
//       delete this.exam.validationErrors['ExamScore'];
//     }
//   } else if (value < 0) {
//     input.value = '0';
//     this.exam.examScore = 0;

//     if (this.exam.validationErrors?.['ExamScore']) {
//       delete this.exam.validationErrors['ExamScore'];
//     }
//   } else {
//     this.exam.examScore = value;

//     if (this.exam.validationErrors?.['ExamScore']) {
//       delete this.exam.validationErrors['ExamScore'];
//     }
//   }
// }

// preventInvalidKeys(event: KeyboardEvent): void {
//   const invalidKeys = ['-', 'e', 'E', '+'];
//   if (invalidKeys.includes(event.key)) {
//     event.preventDefault();
//   }
// }
//   cancel(): void {
//     this.dialogRef.close(null);
//   }
// }



import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { DxSelectBoxModule } from 'devextreme-angular';

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
    DxSelectBoxModule
  ],
})
export class ExamFormComponent implements OnInit {
  exam: ExamDto;
  students: any[] = [];
  lessons: any[] = [];

  constructor(
    public dialogRef: MatDialogRef<ExamFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private http: HttpClient,
    private cdr: ChangeDetectorRef
  ) {
    const formattedDate = data?.ExamDate
      ? this.toDateInputValue(data.ExamDate)
      : '';

    this.exam = {
      id: data?.Id ?? undefined,
      examDate: formattedDate,
      examScore: data?.ExamScore ?? 0,
      studentNumber: data?.StudentNumber ?? 0,
      lessonCode: data?.LessonCode ?? '',
      validationErrors: {}
    };
  }

  ngOnInit(): void {
    if (this.data?.students) {
      this.students = this.data.students;
    } else {
      this.http.get<any[]>('https://localhost:7066/api/v1/students').subscribe(data => {
        this.students = data;
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
        if (!this.exam.lessonCode && this.lessons.length > 0) {
          this.exam.lessonCode = this.lessons[0].Code;
        }
      });
    }
  }

  save(): void {
    this.exam.validationErrors = {};

    let parsedDate: string | null = null;
    if (this.exam.examDate?.trim()) {
      const dateObj = new Date(this.exam.examDate);
      parsedDate = dateObj.toISOString();
    }

    const body = {
      Id: this.exam.id,
      ExamDate: parsedDate,
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
        const errors = err.error?.errors;
        if (err.status === 400 && errors) {
          this.exam.validationErrors = { ...errors };
          this.cdr.detectChanges(); // <-- это важно
        } else if (err.error?.message) {
          this.exam.validationErrors = {
            General: [err.error.message]
          };
        } else {
          this.exam.validationErrors = {
            General: ['Xəta baş verdi. Zəhmət olmasa yenidən cəhd edin']
          };
        }
        this.cdr.detectChanges(); // <-- тоже
      }
    });
  }

  validateScore(event: Event): void {
    const input = event.target as HTMLInputElement;
    let value = parseInt(input.value, 10);
    if (isNaN(value)) value = 0;

    if (value > 700) {
      input.value = '700';
      this.exam.examScore = 700;
    } else if (value < 0) {
      input.value = '0';
      this.exam.examScore = 0;
    } else {
      this.exam.examScore = value;
    }

    if (this.exam.validationErrors?.['ExamScore']) {
      delete this.exam.validationErrors['ExamScore'];
    }
  }

  preventInvalidKeys(event: KeyboardEvent): void {
    const invalidKeys = ['-', 'e', 'E', '+'];
    if (invalidKeys.includes(event.key)) {
      event.preventDefault();
    }
  }

  cancel(): void {
    this.dialogRef.close(null);
  }

  private toDateInputValue(date: string | Date): string {
    const d = new Date(date);
    const year = d.getFullYear();
    const month = ('0' + (d.getMonth() + 1)).slice(-2);
    const day = ('0' + d.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }

  isArray(value: any): boolean {
    return Array.isArray(value);
  }
}
