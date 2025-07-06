import {Component} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Router, RouterModule} from '@angular/router';
import { AuthService } from '../services/auth.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
    selector: 'app-layout',
    standalone: true,
    imports: [
        CommonModule, 
        RouterModule,
        MatSidenavModule,
    MatToolbarModule,
MatListModule,
MatIconModule,
MatButtonModule],
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.css']
})
export class LayoutComponent {
constructor(
    private authService: AuthService,
    private router: Router
){}

logout(){
    this.authService.logout();
    this.router.navigate(['/login']);
}

}


