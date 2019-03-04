import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appImgLoader]',
})
export class ImgLoaderDirective {
  constructor(public viewContainerRef: ViewContainerRef) { }
}