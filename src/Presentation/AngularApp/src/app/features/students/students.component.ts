import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxDataGridModule } from 'devextreme-angular';
import { HttpClient } from '@angular/common/http';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { StudentFormComponent } from './student-form.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-students',
  standalone: true,
  imports: [CommonModule, DxDataGridModule, MatDialogModule, MatIconModule, MatButtonModule],
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {
  students: any[] = [];
  private dialog = inject(MatDialog);

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadStudents();
  }

  loadStudents(): void {
    this.http.get<any[]>('https://localhost:7066/api/v1/students').subscribe({
      next: (data) => {
        this.students = data.map(item => ({
          id: item.Id,
          firstName: item.FirstName,
          surname: item.Surname,
          grade: item.Grade,
          orderNumber: item.OrderNumber
        }));
      },
      error: (err) => {
        console.error('Failed to load students', err);
      }
    });
  }

  editStudent(e: any): void {
    const student = e.row.data;
    this.openForm(student);
  }

  createStudent(): void {
    this.openForm({
      firstName: '',
      surname: '',
      grade: 1,
      orderNumber: 1
    });
  }

  openForm(student: any): void {
    const dialogRef = this.dialog.open(StudentFormComponent, {
      width: '600px',
      maxHeight: '90vh',
      disableClose: true,
      data: student
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.loadStudents();
      }
    });
  }

  deleteStudent(id: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Təsdiq et',
        message: 'Silmek istədiyinizə əminsiniz?'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.http.delete(`https://localhost:7066/api/v1/students/${id}`).subscribe({
          next: () => this.loadStudents(),
          error: err => console.error('Failed to delete student', err)
        });
      }
    });
  }

  onDeleteClick(e: any): void {
    const id = e?.row?.data?.id;
    if (id) {
      this.deleteStudent(id);
    } else {
      console.error('Şagird tapılmadı:', e);
    }
  }

  handleEditButtonClick = (e: any) => this.editStudent(e);
  handleDeleteButtonClick = (e: any) => this.onDeleteClick(e);
}



