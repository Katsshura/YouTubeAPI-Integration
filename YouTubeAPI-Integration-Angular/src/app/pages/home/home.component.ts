import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {YoutubeService} from '../../services/youtube.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router, private youtubeService: YoutubeService) {
    // this.authService.UserSession.subscribe(auth => {
    //   if (!auth) {
    //     this.router.navigate(['login']);
    //   }
    // });
  }

  ngOnInit() {
    // this.authService.singOut();
  }
}
