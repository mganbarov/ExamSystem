// import {Component, Inject} from '@angular/core';
// import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
// import {CommonModule} from '@angular/common';
// import {FormsModule} from '@angular/forms';
// import { MatFormFieldModule } from '@angular/material/form-field';
// import { MatInputModule } from '@angular/material/input';
// import { MatButtonModule } from '@angular/material/button';

// interface LessonDto{
//     id?: number;
//     code: string;
//     name: string;
//     grade: number;
//     teacherFirstName: string;
//     teacherSurname:string;
//     validationErrors?:{[key:string]: string[]};
// }

// @Component({
//     selector: 'app-lesson-form',
//     standalone: true,
//     imports: [
//         CommonModule,
//         FormsModule, 
//         MatFormFieldModule,
//         MatInputModule,
//         MatButtonModule,
//     ],
//     templateUrl: './lesson-form.component.html',
//     styleUrls:['./lesson-form.component.css']
// })

// export class LessonFormComponent{
//     lesson: LessonDto;

//     constructor(
//         public dialogRef: MatDialogRef<LessonFormComponent>,
//         @Inject(MAT_DIALOG_DATA) public data: LessonDto
//     ){
//         console.log('LessonFormComponent DATA:',data);
//         this.lesson = {
//             ...data,
//         validationErrors: data.validationErrors ?? {}};
//     }
//     save(){
//         this.dialogRef.close(this.lesson);
//     }
//     cancel(){
//         this.dialogRef.close(null);
//     }
// }


import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';

interface LessonDto {
  id?: number;
  code: string;
  name: string;
  grade: number;
  teacherFirstName: string;
  teacherSurname: string;
  validationErrors?: { [key: string]: string[] };
}

@Component({
  selector: 'app-lesson-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './lesson-form.component.html',
  styleUrls: ['./lesson-form.component.css']
})
export class LessonFormComponent {
  lesson: LessonDto;

  constructor(
    public dialogRef: MatDialogRef<LessonFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: LessonDto,
    private http: HttpClient,
    private dialog: MatDialog
  ) {
    this.lesson = {
      ...data,
      validationErrors: data.validationErrors ?? {}
    };
  }

  save() {
    this.lesson.validationErrors = {};

    const body = {
      Id: this.lesson.id,
      Code: this.lesson.code,
      Name: this.lesson.name,
      Grade: this.lesson.grade,
      TeacherFirstName: this.lesson.teacherFirstName,
      TeacherSurname: this.lesson.teacherSurname
    };

    const request = body.Id
      ? this.http.put('https://localhost:7066/api/v1/lessons', body)
      : this.http.post('https://localhost:7066/api/v1/lessons', { ...body, Id: undefined });

    request.subscribe({
        next: () => this.dialogRef.close(true),
        error: (err) => {
            if(err.status === 400 && err.error?.errors){
                this.lesson.validationErrors = err.error.errors;
            }
            else{
                alert('Xəta baş verdi. Zəhmət olmasa yenidən cəhd edin.');
            }
        }
    })

  }

  cancel() {
    this.dialogRef.close(null);
  }
}
