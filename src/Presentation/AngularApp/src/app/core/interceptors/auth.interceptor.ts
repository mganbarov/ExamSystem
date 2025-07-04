import {Injectable } from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse} from '@angular/common/http';
import { Observable, throwError} from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../../services/auth.service';
import {Router} from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor{
    constructor(private authService: AuthService, private router: Router){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this.authService.getToken();

        const authReq = token
            ? req.clone({setHeaders: {Authorization: `Bearer ${token}`}})
            : req;
        
        return next.handle(authReq).pipe(
            catchError((error: HttpErrorResponse)=>{
                if(error.status === 401){
                    this.authService.logout();
                    this.router.navigate(['/login']);
                }
                return throwError (()=> error); 
            })
        )



    }


}
