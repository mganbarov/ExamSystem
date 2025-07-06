import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxDataGridModule } from 'devextreme-angular';
import { HttpClient } from '@angular/common/http';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ExamFormComponent } from './exams-form.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-exams',
  standalone: true,
  imports: [CommonModule, DxDataGridModule, MatDialogModule, MatIconModule, MatButtonModule],
  templateUrl:'./exams.component.html',
  styleUrls:['./exams.component.css']
})


export class ExamsComponent implements OnInit {
  exams: any[] = [];
  private dialog = inject(MatDialog);

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadExams();
  }

  loadExams(): void{
    this.http.get<any[]>('https://localhost:7066/api/v1/exams').subscribe({
      next: (data) => {
        this.exams = data.map(item => ({
          id: item.Id,
          examDate: item.ExamDate,
          examScore: item.ExamScore,
          studentNumber: item.StudentNumber,
          lessonCode: item.lessonCode
        }));
      },
      error: (err) => {
        console.log("Failed to load exams", err)
      }
    })
  }

  // openForm(exam: any): void {
  //   const dialogRef = this.dialog.open(ExamFormComponent,{
  //     width: '600px',
  //     maxHeight: '90vh',
  //     disableClose: true,
  //     data: exam
  //   });

  //   dialogRef.afterClosed().subscribe(result => {
  //     if(result === true){
  //       this.loadExams();
  //     }
  //   })

  // }


  openForm(exam: any): void {
  // Загружаем данные студентов и уроков параллельно
  const studentsRequest = this.http.get<any[]>('https://localhost:7066/api/v1/students');
  const lessonsRequest = this.http.get<any[]>('https://localhost:7066/api/v1/lessons');

  studentsRequest.subscribe(students => {
    lessonsRequest.subscribe(lessons => {
      // Открываем диалог только после того, как данные загружены
      const dialogRef = this.dialog.open(ExamFormComponent, {
        width: '600px',
        maxHeight: '90vh',
        disableClose: true,
        data: {
          ...exam,
          students,
          lessons
        }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result === true) {
          this.loadExams();
        }
      });
    });
  });
}


  editExams(e: any) : void{
    const exam = e.row.data;
    this.openForm(exam);
  }

  createExam(): void{
    this.openForm({
      examDate: new Date().toISOString(),
      examScore: 0,
      studentNumber: 1,
      lessonCode: ''
    })
  }

  deleteExam(id: number): void{
    const dialogRef = this.dialog.open(ConfirmDialogComponent,{
      width:'400px',
      data:{
        title: 'Təsdiq et',
        message: 'Silmək istədiyinizə əminsiz?'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this.http.delete(`https://localhost:7066/api/v1/exams/${id}`).subscribe({
          next: () => this.loadExams(),
          error: err => console.error('Failed to delete exam', err)
        });
      }
    });
  }

  onDeleteClick(e: any): void{
    const id = e?.row?.data?.id;
    if(id){
      this.deleteExam(id);
    } else{
      console.error('Exam not found');
    }
  }

  handleEditExamButtonClick = (e: any) => this.editExams(e);
  handleDeleteExamButtonClick = (e: any) => this.onDeleteClick(e);

}
