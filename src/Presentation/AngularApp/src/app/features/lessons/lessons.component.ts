
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-lessons',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule],
  templateUrl: './lessons.component.html',
  styleUrls: ['./lessons.component.css']
})
export class LessonsComponent {
  displayedColumns: string[] = ['id', 'name', 'actions'];
  dataSource = [
    { id: 1, name: 'Lessons 1' },
    { id: 2, name: 'Lessons 2' }
  ];
}
