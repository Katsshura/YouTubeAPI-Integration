import {Directive, ElementRef, HostListener} from '@angular/core';
import {Input} from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})

// You can use this parameter inside html
export class HighlightDirective {

  @Input('appHighlight') color: string;

  constructor(private element: ElementRef) { }

  // HostListener listen to events from DOM
  @HostListener('mouseover') onMouseOver() {
    this.highLightDirective(this.color);
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.highLightDirective(null);
  }

  private highLightDirective(color: string) {
    this.element.nativeElement.style.backgroundColor = color;
  }

}
