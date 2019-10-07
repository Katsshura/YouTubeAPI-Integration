import { Component, OnInit, Input } from '@angular/core';
import {ChannelModel} from '../../models/channel.model';

@Component({
  selector: 'app-channel-info',
  templateUrl: './channel-info.component.html',
  styleUrls: ['./channel-info.component.scss']
})
export class ChannelInfoComponent implements OnInit {

  @Input() channel: ChannelModel;

  constructor() { }

  ngOnInit() { }

}
