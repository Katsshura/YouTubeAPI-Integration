import { Component, OnInit } from '@angular/core';
import { Config as ParticleConfig} from '../../util/particles.config';
import {AuthService} from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(private auth: AuthService, private router: Router, private toastr: ToastrService) {
    this.auth.UserSession.subscribe(user => {
      if (user) {
        this.router.navigate(['home']);
      }
    });
  }

  ngOnInit() {
  }

  public login() {
    this.auth.loginWithGoogle(this.navigateToHome.bind(this));
  }

  private navigateToHome() {
    this.router.navigate(['home']).then(() => {
      this.toastr.success('Welcome, hoping you to enjoy \n share your opinion on github and help to improve', 'Welcome s2');
    });
  }
}
