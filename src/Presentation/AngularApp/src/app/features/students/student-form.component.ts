import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';

interface StudentDto {
  id?: number;
  firstName: string;
  surname: string;
  grade: number;
  orderNumber: number;
  validationErrors?: { [key: string]: string[] };
}

@Component({
  selector: 'app-student-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.css'],
  
})
export class StudentFormComponent {
  student: StudentDto;

  constructor(
    public dialogRef: MatDialogRef<StudentFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: StudentDto,
    private http: HttpClient
  ) {
    this.student = {
      ...data,
      validationErrors: data.validationErrors ?? {}
    };
  }

  save() {
    this.student.validationErrors = {};

    const body = {
      Id: this.student.id,
      FirstName: this.student.firstName,
      Surname: this.student.surname,
      OrderNumber: this.student.orderNumber,
      Grade: this.student.grade
    };

    const request = body.Id
      ? this.http.put('https://localhost:7066/api/v1/students', body)
      : this.http.post('https://localhost:7066/api/v1/students', { ...body, Id: undefined });

    request.subscribe({
      next: () => this.dialogRef.close(true),
      error: err => {
        if (err.status === 400 && err.error?.errors) {
          this.student.validationErrors = err.error.errors;
        } else {
          alert('Xəta baş verdi. Zəhmət olmasa, yenidən cəhd edin.');
        }
      }
    });
  }

  cancel() {
    this.dialogRef.close(null);
  }

  preventInvalidKeys(event: KeyboardEvent): void {
  const invalidKeys = ['-', 'e', 'E', '+'];
  if (invalidKeys.includes(event.key)) {
    event.preventDefault();
  }
}

disableScroll(event: WheelEvent): void {
  (event.target as HTMLInputElement).blur(); 
}

preventPasteNegative(event: ClipboardEvent): void {
  const pasted = event.clipboardData?.getData('text');
  if (pasted && /^-/.test(pasted)) {
    event.preventDefault();
  }
}

allowLettersOnly(event: KeyboardEvent): void {
  const char = event.key;

  
  if (!/^[\p{L} ]$/u.test(char)) {
    event.preventDefault();
  }
}



}
