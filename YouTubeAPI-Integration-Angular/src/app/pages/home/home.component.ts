import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {YoutubeService} from '../../services/youtube.service';
import {ChannelModel} from '../../models/channel.model';
import {PlaylistType} from '../../enums/playlist-type.enum';
import {VideoModel} from '../../models/video.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  private channel: ChannelModel;
  private videos: VideoModel[] = [];

  constructor(private authService: AuthService, private router: Router, private youtubeService: YoutubeService) {
    // this.authService.UserSession.subscribe(auth => {
    //   if (!auth) {
    //     this.router.navigate(['login']);
    //   }
    // });

  }

  ngOnInit() {
    // this.channel = new ChannelModel();
    // this.channel.name = 'Katsshura';
    // this.channel.subscribers = 100;
    // this.channel.videos = 100;
    // this.channel.views = 800;
    // this.channel.thumbnails = { medium: { url: 'https://i.pinimg.com/originals/f3/6d/4d/f36d4dbec4f480a4d8bc16cebf547ae9.jpg'}};
    //
    // console.log(this.channel);
    // const video = new VideoModel();
    // video.channelName = 'Katsshura\'s development';
    // video.title = 'How to be a prostitute nowadays';
    // video.thumbnails = 'https://i.pinimg.com/originals/f3/6d/4d/f36d4dbec4f480a4d8bc16cebf547ae9.jpg';
    // video.duration = '12:00:00';
    // video.comments = 90;
    // video.likes = 10000;
    // video.dislikes = 3000;
    // video.favorites = 900;
    // video.views = 1200000;
    //
    // this.videos.push(video);
    // this.videos.push(video);
    // this.videos.push(video);
    this.getChannel();
    // this.authService.singOut();
  }

  private logout() {
    this.authService.singOut(res => {
      this.router.navigate(['login']);
    });
  }

  private async getChannel() {
    this.channel = await this.youtubeService.getChannelInformation(this.onGoogleTokenExpired.bind(this));
    this.getPlaylist(PlaylistType.Upload);
  }

  private async getPlaylist(playlistType: string | PlaylistType) {
    const playlist = PlaylistType[playlistType];
    this.videos = await this.youtubeService.getPlaylistVideos(playlist, this.onGoogleTokenExpired.bind(this));
  }

  private onGoogleTokenExpired(res: any) {
    this.router.navigate(['login']);
    // TODO: Implement toast message for expiration time
  }
}
