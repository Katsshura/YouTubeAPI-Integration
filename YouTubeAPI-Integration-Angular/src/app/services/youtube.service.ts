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
  private readonly prefetch = 10;
  private pageToken = null;

  public get PageToken() { return this.pageToken; }

  constructor(private auth: AuthService, private http: HttpClient) {  }

  public async getPlaylistVideos(playlistType: PlaylistType, unauthorized: Function, error: Function) {
    const request_url = `${this.api_base_url}/api/home/playlist?playlistType=${playlistType}`;
    const options = {
      headers: this.getHeader(true)
    };
    return new Promise<VideoModel[]>((resolve) => {
      this.http.get<VideoModel[]>(request_url, options).toPromise().then(
        res => {
          this.pageToken = res['key'];
          resolve(res['value']);
          },

        err => {
        if (err.status === 401) {
          this.auth.accessTokenExpired(unauthorized);
        }
        error(err);
      });
    });
  }

  public async getChannelInformation(unauthorized: Function, error: Function) {
    const request_url = `${this.api_base_url}/api/home/channel`;
    const options = {
      headers: this.getHeader(true)
    };
    return new Promise<ChannelModel>((resolve) => {
      this.http.get<ChannelModel>(request_url, options).toPromise().then(res => resolve(res), err => {
        if (err.status === 401) {
          this.auth.accessTokenExpired(unauthorized);
        }
        error(err);
      });
    });
  }

  public resetPageToken() {
    this.pageToken = null;
  }

  private getHeader(oauthToken: boolean): HttpHeaders {
    const httpHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      'Cache-Control': 'no-cache',
      'pageToken': `${this.pageToken || '\" \"'}`,
      'prefetch': `${this.prefetch}`,
    });

    if (oauthToken) {
      return httpHeader.append('oauthToken', this.auth.Token);
    }

    return httpHeader;
  }
}
