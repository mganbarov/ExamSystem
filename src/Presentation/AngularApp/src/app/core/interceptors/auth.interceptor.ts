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
        console.log("Intercepter request:", req.url);
        const token = this.authService.getToken();
        console.log(token);
        const authReq = token
            ? req.clone({setHeaders: {Authorization: `Bearer ${token}`}})
            : req;
        console.log('Interceptor token:', token);
        return next.handle(authReq).pipe(
            catchError((error: HttpErrorResponse)=>{
                console.log('Interceptor error:', error);
                if(error.status===401 &&
                    !req.url.includes('/auth/login')
                ){
                    console.warn('Token is invalid or expired. Logging out...');
                    this.authService.logout();

                    if(this.router.url !== '/login'){
                        this.router.navigate(['/login'])
                    }

                }
                // if(error.status === 401){
                //     this.authService.logout();
                //     this.router.navigate(['/login']);
                // }
                return throwError (()=> error); 
            })
        )



    }


}
