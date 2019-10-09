import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-loading-page',
  templateUrl: './loading-page.component.html',
  styleUrls: ['./loading-page.component.scss']
})
export class LoadingPageComponent implements OnInit, OnDestroy {

  @Input() isLoading !: boolean;
  private message = LoadingPageMessages[0];
  private timerId = 0;
  private seconds = 0;

  constructor() { }

  ngOnInit(): void {
    this.timerId = setInterval(() => {
      this.seconds++;
      if (this.seconds % 10 === 0) {
        this.message = this.setMessageBasedOnTime();
      }
      console.log(this.seconds);
    }, 1000);
  }

  ngOnDestroy(): void {
    clearInterval(this.timerId);
  }

  private setMessageBasedOnTime(): string {
    if (this.seconds >= 10 && this.seconds < 20) {
      return LoadingPageMessages[1];
    } else if (this.seconds >= 20 && this.seconds < 30) {
      return LoadingPageMessages[2];
    } else if (this.seconds >= 30 && this.seconds < 40) {
      return LoadingPageMessages[3];
    } else if (this.seconds >= 40) {
      return LoadingPageMessages[4];
    }
  }

}

const LoadingPageMessages = [
  'Order received, now Loading captain',
  'Please wait.. We\'re giving our best to process your request D:',
  'Be patient, take a breath and spread love s2',
  'Hm... Seen\'s like you have a lot of videos here hehe',
  'Oooops..! Seens like our servers are struggling to handle your request :(',
  'Sooo... how has been your day?... :p'
];
