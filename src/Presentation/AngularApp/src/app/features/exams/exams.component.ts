
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-exams',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule],
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.css']
})
export class ExamsComponent {
  displayedColumns: string[] = ['id', 'name', 'actions'];
  dataSource = [
    { id: 1, name: 'Exams 1' },
    { id: 2, name: 'Exams 2' }
  ];
}
