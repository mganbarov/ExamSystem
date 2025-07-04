import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

interface LoginRequest{
    username:string;
    password:string;
}

interface LoginResponse{
    token:string;
}

@Injectable({
    providedIn:"root",
})
export class AuthService{
    private apiUrl = 'https://localhost:7066/api/v1/auth/login';

    constructor(private http: HttpClient){}
    login(data: LoginRequest) : Observable<string>{
        return this.http.post(this.apiUrl, data, {responseType: 'text'});
    }

    saveToken(token: string){
        localStorage.setItem('auth_token',token);
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