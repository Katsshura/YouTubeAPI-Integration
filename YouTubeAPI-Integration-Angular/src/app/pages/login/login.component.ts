import { Component, OnInit } from '@angular/core';
import { Config as ParticleConfig} from '../../util/particles.config';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  private myParams = ParticleConfig;
  constructor(private auth: AuthService, private router: Router) {
    this.auth.UserSession.subscribe(user => {
      if (user) {
        this.router.navigate(['home']);
      }
    });
  }

  ngOnInit() {
  }

  public login() {
    this.auth.loginWithGoogle();
  }
}
