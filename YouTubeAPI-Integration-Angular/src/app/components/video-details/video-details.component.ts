import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {VideoModel} from '../../models/video.model';

@Component({
  selector: 'app-video-details',
  templateUrl: './video-details.component.html',
  styleUrls: ['./video-details.component.scss']
})
export class VideoDetailsComponent implements OnInit {

  @Input() video: VideoModel;

  constructor() { }

  ngOnInit() {
    console.log(this.video);
  }

}
