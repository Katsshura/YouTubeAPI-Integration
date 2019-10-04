import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {YoutubeService} from '../../services/youtube.service';
import {ChannelModel} from '../../models/channel.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  private channel: ChannelModel;

  constructor(private authService: AuthService, private router: Router, private youtubeService: YoutubeService) {
    // this.authService.UserSession.subscribe(auth => {
    //   if (!auth) {
    //     this.router.navigate(['login']);
    //   }
    // });
  }

  ngOnInit() {
    this.getChannel();
    // this.authService.singOut();
  }

  private logout() {
    this.authService.singOut(res => {
      console.log(res);
      this.router.navigate(['login']);
    });
  }

  private async getChannel() {
    this.channel = await this.youtubeService.getChannelInformation();
    console.log(this.channel);
  }
}
