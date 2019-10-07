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

  constructor(private auth: AuthService, private http: HttpClient) {  }

  public async getPlaylistVideos(playlistType: PlaylistType, unauthorized: Function) {
    const request_url = `${this.api_base_url}/api/home/playlist?playlistType=${playlistType}`;
    const options = {
      headers: this.getHeader(true)
    };
    return new Promise<VideoModel[]>((resolve, reject) => {
      this.http.get<VideoModel[]>(request_url, options).toPromise().then(res => resolve(res), err => {
        if (err.status === 401) {
          this.auth.accessTokenExpired(unauthorized);
        }
        console.log(err);
      });
    });
  }

  public async getChannelInformation(unauthorized: Function) {
    const request_url = `${this.api_base_url}/api/home/channel`;
    const options = {
      headers: this.getHeader(true)
    };
    return new Promise<ChannelModel>((resolve, reject) => {
      this.http.get<ChannelModel>(request_url, options).toPromise().then(res => resolve(res), err => {
        if (err.status === 401) {
          this.auth.accessTokenExpired(unauthorized);
        }
        console.log(err);
      });
    });
  }

  private getHeader(oauthToken: boolean): HttpHeaders {
    const httpHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      'Cache-Control': 'no-cache',
    });

    if (oauthToken) {
      return httpHeader.append('oauthToken', this.auth.Token);
    }

    return httpHeader;
  }
}
