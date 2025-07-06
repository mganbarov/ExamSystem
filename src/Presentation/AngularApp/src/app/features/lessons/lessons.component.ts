// import { Component, OnInit, inject } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { HttpClient } from '@angular/common/http';

// import { DxDataGridModule, DxButtonModule } from 'devextreme-angular';
// import { MatDialog, MatDialogModule } from '@angular/material/dialog';
// import { MatIconModule } from '@angular/material/icon';
// import { MatButtonModule } from '@angular/material/button';

// import { LessonFormComponent } from './lesson-form.component';
// import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';

// @Component({
//   selector: 'app-lessons',
//   standalone: true,
//   templateUrl: './lessons.component.html',
//   styleUrls: ['./lessons.component.css'],
//   imports: [
//     CommonModule,
//     DxDataGridModule,
//     DxButtonModule,
//     MatDialogModule,
//     MatIconModule,
//     MatButtonModule
//   ]
// })
// export class LessonsComponent implements OnInit {
//   lessons: any[] = [];
//   private dialog = inject(MatDialog);

//   constructor(private http: HttpClient) {}

//   ngOnInit(): void {
//     this.loadLessons();
//   }

//   loadLessons(): void {
//     this.http.get<any[]>('https://localhost:7066/api/v1/lessons').subscribe({
//       next: (data) => {
//         this.lessons = data.map(item => ({
//           id: item.Id,
//           code: item.Code,
//           name: item.Name,
//           grade: item.Grade,
//           teacherFirstName: item.TeacherFirstName,
//           teacherSurname: item.TeacherSurname,
//         }));
//       },
//       error: (err) => {
//         console.error('Failed to load lessons', err);
//       }
//     });
//   }

//   createLesson(): void {
//     this.openForm({
//       code: '',
//       name: '',
//       grade: 1,
//       teacherFirstName: '',
//       teacherSurname: ''
//     });
//   }

//   editLesson(e: any): void {
//     console.log("e:", e);
//     console.log("e.row:",e.row);
//     console.log("e.row.data", e.row.data);
//     const lesson = e.row.data;
//     this.openForm(lesson);
//   }

//   openForm(lesson: any): void {
//     const dialogRef = this.dialog.open(LessonFormComponent, {
//       width: '600px',
//       maxHeight: '90vh',
//       disableClose: true,
//       data: lesson
//     });

//     dialogRef.afterClosed().subscribe(result => {
//       if (result) {
//         const body = {
//           Id: result.id,
//           Code: result.code,
//           Name: result.name,
//           Grade: result.grade,
//           TeacherFirstName: result.teacherFirstName,
//           TeacherSurname: result.teacherSurname
//         };

//         if (result.id) {
//           this.http.put('https://localhost:7066/api/v1/lessons', body).subscribe({
//             next: () => this.loadLessons(),
//             error: err => this.handleValidationErrors(err, body)
              
            
//           });
//         } else {
//           delete body.Id;
//           this.http.post('https://localhost:7066/api/v1/lessons', body).subscribe({
//             next: () => this.loadLessons(),
//             error: err => {
//               //console.error('POST error response:', err);
//              //console.log('Validation errors', err.error?.errors);
//               this.handleValidationErrors(err, body)
              
//             }
//             });
//         }
//       }
//     });
//   }

//   handleValidationErrors(err: any, body: any): void {
//     if (err.status === 400 && err.error?.errors) {
//       this.dialog.open(LessonFormComponent, {
//         width: '600px',
//         maxHeight: '90vh',
//         disableClose: true,
//         data: {
//           ...body,
//           validationErrors: err.error.errors
//         }
//       });
//     } else {
//       alert('Xəta baş verdi. Təkrar yoxlayın.');
//     }
//   }

//   deleteLesson(id: number): void {
//     const dialogRef = this.dialog.open(ConfirmDialogComponent, {
//       width: '400px',
//       data: {
//         title: 'Təsdiq et',
//         message: 'Silmək istədiyinizə əminsiniz?'
//       }
//     });

//     dialogRef.afterClosed().subscribe(result => {
//       if (result) {
//         this.http.delete(`https://localhost:7066/api/v1/lessons/${id}`).subscribe({
//           next: () => this.loadLessons(),
//           error: err => console.error('Failed to delete lesson', err)
//         });
//       }
//     });
//   }

//   onDeleteClick(e: any): void {
//     const id = e?.row?.data?.id;
//     if (id) {
//       this.deleteLesson(id);
//     } else {
//       console.error('Dərs tapılmadı:', e);
//     }
//   }

//   handleEditLessonButtonClick=(e: any) => this.editLesson(e);
  
//   handleDeleteLessonButtonClick =(e: any) => this.onDeleteClick(e);

// }


import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

import { DxDataGridModule, DxButtonModule } from 'devextreme-angular';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

import { LessonFormComponent } from './lesson-form.component';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-lessons',
  standalone: true,
  templateUrl: './lessons.component.html',
  styleUrls: ['./lessons.component.css'],
  imports: [
    CommonModule,
    DxDataGridModule,
    DxButtonModule,
    MatDialogModule,
    MatIconModule,
    MatButtonModule
  ]
})
export class LessonsComponent implements OnInit {
  lessons: any[] = [];
  private dialog = inject(MatDialog);

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadLessons();
  }

  loadLessons(): void {
    this.http.get<any[]>('https://localhost:7066/api/v1/lessons').subscribe({
      next: (data) => {
        this.lessons = data.map(item => ({
          id: item.Id,
          code: item.Code,
          name: item.Name,
          grade: item.Grade,
          teacherFirstName: item.TeacherFirstName,
          teacherSurname: item.TeacherSurname
        }));
      },
      error: (err) => {
        console.error('Failed to load lessons', err);
      }
    });
  }

  createLesson(): void {
    this.openForm({
      code: '',
      name: '',
      grade: 1,
      teacherFirstName: '',
      teacherSurname: ''
    });
  }

  editLesson(e: any): void {
    const lesson = e.row.data;
    this.openForm(lesson);
  }

  openForm(lesson: any): void {
    const dialogRef = this.dialog.open(LessonFormComponent, {
      width: '600px',
      maxHeight: '90vh',
      disableClose: true,
      data: lesson
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.loadLessons();
      }
    });
  }

  deleteLesson(id: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Təsdiq et',
        message: 'Silmək istədiyinizə əminsiniz?'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.http.delete(`https://localhost:7066/api/v1/lessons/${id}`).subscribe({
          next: () => this.loadLessons(),
          error: err => console.error('Failed to delete lesson', err)
        });
      }
    });
  }

  onDeleteClick(e: any): void {
    const id = e?.row?.data?.id;
    if (id) {
      this.deleteLesson(id);
    } else {
      console.error('Dərs tapılmadı:', e);
    }
  }

  handleEditLessonButtonClick = (e: any) => this.editLesson(e);
  handleDeleteLessonButtonClick = (e: any) => this.onDeleteClick(e);
}
