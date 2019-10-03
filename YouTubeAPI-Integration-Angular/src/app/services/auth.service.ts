import { Injectable } from '@angular/core';
import {Router} from '@angular/router';
import {AngularFireAuth} from '@angular/fire/auth';
import {Observable} from 'rxjs';
import {GoogleScopes} from '../util/enums/google-scopes';

import * as firebase from 'firebase';
import GoogleAuthProvider = firebase.auth.GoogleAuthProvider;
import Auth = firebase.auth.Auth;

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly _user: Observable<firebase.User>;
  private readonly googleProvider: GoogleAuthProvider;
  private readonly auth: Auth = firebase.auth();
  private readonly token_key = 'oauthToken';

  public get UserSession(): Observable<firebase.User> { return this._user; }
  public get Token(): string { return localStorage.getItem(this.token_key); }

  constructor(private router: Router, private fireAuth: AngularFireAuth) {
    this.googleProvider = this.configGoogleProvider(GoogleScopes.YouTubeGoogleAPI.YouTube);
    this._user = fireAuth.authState;
  }

  public loginWithGoogle() {
    if (!this.auth.currentUser) {
      this.auth.signInWithPopup(this.googleProvider).then(res => {
        this.saveToken(res.credential['accessToken']);
        console.log(this.Token);
        this.router.navigate(['home']);
      });
    }
  }

  public singOut() {
    this.auth.signOut().then(res => console.log(res));
  }

  private configGoogleProvider(...scopes: string[]): GoogleAuthProvider {
    const provider = new firebase.auth.GoogleAuthProvider();
    scopes.forEach(scope => provider.addScope(scope));
    return provider;
  }

  private saveToken(token: string) {
    localStorage.setItem(this.token_key, token);
  }
}
