import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {YoutubeService} from '../../services/youtube.service';
import {ToastrService} from 'ngx-toastr';
import {ChannelModel} from '../../models/channel.model';
import {VideoModel} from '../../models/video.model';
import {PlaylistType} from '../../enums/playlist-type.enum';
import {Router} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  private channel: ChannelModel;
  private filtratedVideos: VideoModel[] = [];
  private rawVideos: VideoModel[] = [];
  private filter: string;
  private isLoading = false;
  private lastPlaylist = PlaylistType.Upload;

  constructor(private authService: AuthService,
              private router: Router,
              private youtubeService: YoutubeService,
              private toastr: ToastrService) {
    this.authService.UserSession.subscribe(auth => {
      if (!auth) {
        this.router.navigate(['login'])
          .then(() => {
            this.toastr.error('You\'re not authorized! Please Login first', 'Unauthorized');
          });
      }
    });
  }

  ngOnInit() {
    this.getChannel();
  }

  private logout() {
    this.authService.singOut(this.onLogout.bind(this));
  }

  private async getChannel() {
    this.isLoading = true;
    this.channel = await this.youtubeService.getChannelInformation(this.onGoogleTokenExpired.bind(this), this.onRequestFailed.bind(this));
    this.getPlaylistVideos(PlaylistType.Upload);
  }

  private async getPlaylistVideos(playlistType: string | PlaylistType) {
    this.youtubeService.resetPageToken();
    this.lastPlaylist = PlaylistType[playlistType];
    this.rawVideos = [];
    this.filter = undefined;
    await this.getVideos();
  }

  private async onShowMore() {
    await this.getVideos();
  }

  private async getVideos() {
    this.isLoading = true;
    const res = await this.youtubeService
      .getPlaylistVideos(this.lastPlaylist, this.onGoogleTokenExpired.bind(this), this.onRequestFailed.bind(this));
    this.rawVideos = this.rawVideos.concat(res);
    this.filterVideoList(this.filter);
    this.isLoading = false;
  }

  private filterVideoList(filter: string) {
    if (filter) {
      this.filtratedVideos =  this.rawVideos.filter(item =>
        item.channelName.toLowerCase().indexOf(filter.toLowerCase()) >= 0
        || item.duration.toLowerCase().indexOf(filter.toLowerCase()) >= 0
        || item.title.toLowerCase().indexOf(filter.toLowerCase()) >= 0
        || item.comments.toString().toLowerCase().indexOf(filter.toLowerCase()) >= 0
        || item.likes.toString().toLowerCase().indexOf(filter.toLowerCase()) >= 0
        || item.dislikes.toString().toLowerCase().indexOf(filter.toLowerCase()) >= 0
        || item.favorites.toString().toLowerCase().indexOf(filter.toLowerCase()) >= 0
        || item.views.toString().toLowerCase().indexOf(filter.toLowerCase()) >= 0
      );
    } else {
      this.filtratedVideos = this.rawVideos;
    }
    console.log(this.filtratedVideos);
  }

  private onGoogleTokenExpired(err: any) {
    this.router.navigate(['login'])
      .then(() => {
        this.toastr.error('Ooops.. Your google authorization has expired :( \n Please Login again to refresh it', 'Unauthorized');
      });
  }

  private onRequestFailed(err: any) {
    this.isLoading = false;
    const statusCode = err.status === 0 && err.statusText === 'Unknown Error' ? 404 : err.status;
    this.toastr.error(`Ooops.. We got and error (${statusCode}) while trying to process your request :(`,
      'Sorry, we got an error!');
  }

  private hasMoreVideosToLoad(): boolean {
    return this.youtubeService.PageToken && this.youtubeService.PageToken !== '\" \"';
  }

  private hasVideos(): boolean {
    return this.filtratedVideos && this.filtratedVideos.length > 0;
  }

  private onLogout(res: any) {
    this.router.navigate(['login']);
  }
}
