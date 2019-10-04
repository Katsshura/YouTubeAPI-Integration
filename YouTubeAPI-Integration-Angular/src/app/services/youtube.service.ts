import { Injectable } from '@angular/core';
import {AuthService} from './auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {PlaylistType} from '../enums/playlist-type.enum';
import {VideoModel} from '../models/video.model';
import {ChannelModel} from '../models/channel.model';

@Injectable({
  providedIn: 'root'
})
export class YoutubeService {
  private readonly api_base_url = 'http://localhost:54870';
  private readonly httpHeaders: HttpHeaders;

  constructor(auth: AuthService, private http: HttpClient) {
    this.httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Cache-Control': 'no-cache',
      'oauthToken': `${auth.Token}`
    });
  }

  public async getPlaylistVideos(playlistType: PlaylistType) {
    const request_url = `${this.api_base_url}/api/home/playlist?playlistType=${playlistType}`;
    const options = {
      headers: this.httpHeaders
    };
    return new Promise<VideoModel[]>((resolve, reject) => {
      this.http.get<VideoModel[]>(request_url, options).toPromise().then(res => resolve(res));
    });
  }

  public async getChannelInformation() {
    const request_url = `${this.api_base_url}/api/home/channel`;
    const options = {
      headers: this.httpHeaders
    };
    return new Promise<ChannelModel>((resolve, reject) => {
      this.http.get<ChannelModel>(request_url, options).toPromise().then(res => resolve(res));
    });
  }
}
