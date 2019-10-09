import { BrowserModule } from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AngularFireModule } from '@angular/fire';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AppRoutingModule } from './app-routing.module';
import { MatGridListModule } from '@angular/material/grid-list';
import { ParticlesModule } from 'angular-particle';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './pages/root/app.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ChannelInfoComponent } from './components/channel-info/channel-info.component';
import { VideoDetailsComponent } from './components/video-details/video-details.component';
import { LoadingPageComponent } from './components/loading-page/loading-page.component';

import {AuthService} from './services/auth.service';
import {YoutubeService} from './services/youtube.service';
import { HighlightDirective } from './directives/highlight.directive';



import {registerLocaleData} from '@angular/common';
import ptBr from '@angular/common/locales/pt';
import {environment} from '../environments/environment';
registerLocaleData(ptBr, 'pt-BR');

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    HighlightDirective,
    ChannelInfoComponent,
    VideoDetailsComponent,
    LoadingPageComponent
  ],
  imports: [
    BrowserModule,
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFireAuthModule,
    AppRoutingModule,
    FormsModule,
    MatGridListModule,
    ParticlesModule,
    FontAwesomeModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    AuthService,
    YoutubeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
