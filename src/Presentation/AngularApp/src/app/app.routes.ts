import {Routes} from '@angular/router';
import {LoginComponent} from './login/login.component';
import { AuthGuard } from './core/guards/auth.guard';
import { StudentsComponent } from './features/students/students.component';
import { LessonsComponent } from './features/lessons/lessons.component';
import { ExamsComponent } from './features/exams/exams.component';
import { LayoutComponent } from './layout/layout.component';

export const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {
    path: 'dashboard',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children:[
      {path: 'students', component: StudentsComponent},
      {path: 'lessons', component: LessonsComponent},
      {path: 'exams', component: ExamsComponent},
      {path:'', redirectTo:'students', pathMatch: 'full'}
    ]
  },
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path:'**', redirectTo: 'login'}
];