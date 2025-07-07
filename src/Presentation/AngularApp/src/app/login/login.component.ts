import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule
  ]
})
export class LoginComponent {
  username = '';
  password = '';
  loginError = '';

  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  onSubmit() {
    this.loginError = '';

    this.authService.login({ username: this.username, password: this.password }).subscribe({
      next: (token: string) => {
        
        this.authService.saveToken(token);

        
        this.snackBar.open('Login successful', 'Close', { duration: 3000 });

        
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.loginError = err.error || 'Login failed. Please check your credentials.';
      }
    });
  }
}
