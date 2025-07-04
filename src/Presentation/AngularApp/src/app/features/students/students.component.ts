
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-students',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule],
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent {
  displayedColumns: string[] = ['id', 'name', 'actions'];
  dataSource = [
    { id: 1, name: 'Students 1' },
    { id: 2, name: 'Students 2' }
  ];
}
