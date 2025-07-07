import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();
    const authReq = token
      ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
      : req;

    return next.handle(authReq).pipe(
      catchError((error: HttpErrorResponse) => {
        console.log('Interceptor error:', error);

        if (error.status === 401 && !req.url.includes('/auth/login')) {
          this.authService.logout();
          if (this.router.url !== '/login') {
            this.router.navigate(['/login']);
          }
        }

        if (error.status === 400 && error.error?.errors) {
  const normalizedErrors: { [key: string]: string[] } = {};

  for (const key in error.error.errors) {
    if (!Object.prototype.hasOwnProperty.call(error.error.errors, key)) continue;

    let newKey = key;

    if (newKey.startsWith('$.')) {
      newKey = newKey.substring(2);
    } else if (newKey.toLowerCase() === 'dto') {
      newKey = 'General';
    }

    
    newKey = newKey.charAt(0).toUpperCase() + newKey.slice(1);

    normalizedErrors[newKey] = error.error.errors[key];
  }

  error = {
    ...error,
    error: {
      ...error.error,
      errors: normalizedErrors
    }
  };

  console.log('Normalized validation errors:', normalizedErrors);
}


        return throwError(() => error);
      })
    );
  }
}
