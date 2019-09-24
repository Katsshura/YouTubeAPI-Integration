import { Component } from '@angular/core';
import {Config as ParticleConfig} from '../../util/particles.config';

export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'YouTubeAPI-Integration-Angular';
  tiles: Tile[] = [
    {text: 'One', cols: 3, rows: 1, color: 'lightblue'},
    {text: 'Two', cols: 1, rows: 2, color: 'lightgreen'},
    {text: 'Three', cols: 1, rows: 1, color: 'lightpink'},
    {text: 'Four', cols: 2, rows: 1, color: '#DDBDF1'},
    {text: 'Five', cols: 2, rows: 1, color: '#DDBDF1'},
    {text: 'Six', cols: 2, rows: 1, color: '#DDBDF1'},
    {text: 'Seven', cols: 2, rows: 1, color: '#DDBDF1'},
    {text: 'Seven', cols: 2, rows: 1, color: '#DDBDF1'},
  ];
  private particlesConfig = ParticleConfig;
}
