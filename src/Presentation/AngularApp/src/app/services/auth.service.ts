import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import {map} from 'rxjs/operators';
interface LoginRequest{
    username:string;
    password:string;
}

interface LoginResponse{
    Token:string;
}

@Injectable({
    providedIn:"root",
})
export class AuthService{
    private apiUrl = 'https://localhost:7066/api/v1/auth/login';

    constructor(private http: HttpClient){}
    
    login(data: LoginRequest) : Observable<string>{
        return this.http.post<LoginResponse>(this.apiUrl, data).pipe(
            map(response => response.Token)
        );
    }

   saveToken(token: string | {token:string}){
        const value = typeof token === 'string' ? token : token.token;
        localStorage.setItem('auth_token',value);
   }
    getToken(): string | null {
        return localStorage.getItem('auth_token');
    }

    isLoggedIn() : boolean{
        return !!this.getToken();
    }

    logout() {
        localStorage.removeItem('auth_token');
    }

}