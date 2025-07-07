// import { Component, OnInit, inject } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { DxDataGridModule } from 'devextreme-angular';
// import CustomStore from 'devextreme/data/custom_store';
// import { HttpClient, HttpParams } from '@angular/common/http';
// import { MatDialog, MatDialogModule } from '@angular/material/dialog';
// import { ExamFormComponent } from './exams-form.component';
// import { MatIconModule } from '@angular/material/icon';
// import { MatButtonModule } from '@angular/material/button';
// import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';

// @Component({
//   selector: 'app-exams',
//   standalone: true,
//   imports: [
//     CommonModule,
//     DxDataGridModule,
//     MatDialogModule,
//     MatIconModule,
//     MatButtonModule
//   ],
//   templateUrl: './exams.component.html',
//   styleUrls: ['./exams.component.css']
// })
// export class ExamsComponent implements OnInit {
//   dataSource: CustomStore;
//   private dialog = inject(MatDialog);

//   constructor(private http: HttpClient) {}

//   ngOnInit(): void {
//     this.initDataSource();
//   }

//   initDataSource(): void {
//     this.dataSource = new CustomStore({
//       key: 'Id',
//       load: (loadOptions) => {
//         const page = (loadOptions.skip! / loadOptions.take!) + 1;
//         const pageSize = loadOptions.take!;

//         const params = new HttpParams()
//           .set('page', page.toString())
//           .set('pageSize', pageSize.toString());

//         return this.http
//           .get<any>('https://localhost:7066/api/v1/exams', { params })
//           .toPromise()
//           .then((response) => ({
//             data: response.items,
//             totalCount: response.totalCount
//           }));
//       }
//     });
//   }

//   openForm(exam: any): void {
//     const studentsRequest = this.http.get<any[]>('https://localhost:7066/api/v1/students');
//     const lessonsRequest = this.http.get<any[]>('https://localhost:7066/api/v1/lessons');

//     studentsRequest.subscribe((students) => {
//       lessonsRequest.subscribe((lessons) => {
//         const dialogRef = this.dialog.open(ExamFormComponent, {
//           width: '600px',
//           maxHeight: '90vh',
//           disableClose: true,
//           data: {
//             ...exam,
//             students,
//             lessons
//           }
//         });

//         dialogRef.afterClosed().subscribe((result) => {
//           if (result === true) {
//             this.reloadDataSource();
//           }
//         });
//       });
//     });
//   }

//   reloadDataSource(): void {
//     if (this.dataSource) {
//       (this.dataSource as any).load();
//     }
//   }

//   createExam(): void {
//     this.openForm({
//       examDate: new Date().toISOString(),
//       examScore: 0,
//       studentNumber: 1,
//       lessonCode: ''
//     });
//   }

//   editExams(e: any): void {
//     const exam = e.row.data;
//     this.openForm(exam);
//   }

//   deleteExam(id: number): void {
//     const dialogRef = this.dialog.open(ConfirmDialogComponent, {
//       width: '400px',
//       data: {
//         title: 'Təsdiq et',
//         message: 'Silmək istədiyinizə əminsiz?'
//       }
//     });

//     dialogRef.afterClosed().subscribe((result) => {
//       if (result) {
//         this.http.delete(`https://localhost:7066/api/v1/exams/${id}`).subscribe({
//           next: () => this.reloadDataSource(),
//           error: (err) => console.error('Failed to delete exam', err)
//         });
//       }
//     });
//   }

//   onDeleteClick = (e: any): void => {
//     const id = e?.row?.data?.Id;
//     if (id) {
//       this.deleteExam(id);
//     } else {
//       console.error('Exam not found');
//     }
//   };

//   handleEditExamButtonClick = (e: any) => this.editExams(e);
//   handleDeleteExamButtonClick = (e: any) => this.onDeleteClick(e);
// }



import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxDataGridComponent, DxDataGridModule } from 'devextreme-angular';
import CustomStore from 'devextreme/data/custom_store';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

import { ExamFormComponent } from './exams-form.component';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-exams',
  standalone: true,
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.css'],
  imports: [
    CommonModule,
    DxDataGridModule,
    MatDialogModule,
    MatIconModule,
    MatButtonModule
  ]
})
export class ExamsComponent implements OnInit {
  dataSource: CustomStore;
  private dialog = inject(MatDialog);
  private http = inject(HttpClient);

  @ViewChild(DxDataGridComponent, { static: false })
  grid!: DxDataGridComponent;

  ngOnInit(): void {
    this.initDataSource();
  }

  initDataSource(): void {
    this.dataSource = new CustomStore({
      key: 'Id',
      load: (loadOptions) => {
        const page = (loadOptions.skip! / loadOptions.take!) + 1;
        const pageSize = loadOptions.take!;

        const params = new HttpParams()
          .set('page', page.toString())
          .set('pageSize', pageSize.toString());

        return this.http
          .get<any>('https://localhost:7066/api/v1/exams', { params })
          .toPromise()
          .then((response) => ({
            data: response.items,
            totalCount: response.totalCount
          }));
      }
    });
  }

  reloadDataSource(): void {
    if (this.grid?.instance) {
      this.grid.instance.refresh();
    }
  }

  openForm(exam: any): void {
    const students$ = this.http.get<any[]>('https://localhost:7066/api/v1/students');
    const lessons$ = this.http.get<any[]>('https://localhost:7066/api/v1/lessons');

    students$.subscribe((students) => {
      lessons$.subscribe((lessons) => {
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

        dialogRef.afterClosed().subscribe((result) => {
          if (result === true) {
            this.reloadDataSource();
          }
        });
      });
    });
  }

  createExam(): void {
    this.openForm({
      examDate: new Date().toISOString(),
      examScore: 0,
      studentNumber: 1,
      lessonCode: ''
    });
  }

  editExams(e: any): void {
    const exam = e.row?.data;
    if (exam) {
      this.openForm(exam);
    }
  }

  deleteExam(id: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Təsdiq et',
        message: 'Silmək istədiyinizə əminsiz?'
      }
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.http.delete(`https://localhost:7066/api/v1/exams/${id}`).subscribe({
          next: () => this.reloadDataSource(),
          error: (err) => console.error('Failed to delete exam', err)
        });
      }
    });
  }

  onDeleteClick = (e: any): void => {
    const id = e?.row?.data?.Id;
    if (id) {
      this.deleteExam(id);
    } else {
      console.error('Exam not found');
    }
  };

  handleEditExamButtonClick = (e: any): void => this.editExams(e);
  handleDeleteExamButtonClick = (e: any): void => this.onDeleteClick(e);
}
