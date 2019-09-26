import { BrowserModule } from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AngularFireModule } from '@angular/fire';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AppRoutingModule } from './app-routing.module';
import { MatGridListModule } from '@angular/material/grid-list';
import { ParticlesModule } from 'angular-particle';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './pages/root/app.component';
import { LoginComponent } from './pages/login/login.component';

import {AuthService} from './services/auth.service';



import {registerLocaleData} from '@angular/common';
import ptBr from '@angular/common/locales/pt';
import { HomeComponent } from './pages/home/home.component';
import {environment} from '../environments/environment';
import { HighlightDirective } from './directives/highlight.directive';
registerLocaleData(ptBr, 'pt-BR');

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    HighlightDirective
  ],
  imports: [
    BrowserModule,
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFireAuthModule,
    AppRoutingModule,
    FormsModule,
    MatGridListModule,
    ParticlesModule,
    FontAwesomeModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
