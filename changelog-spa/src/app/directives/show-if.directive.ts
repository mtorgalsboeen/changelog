import { Directive, ElementRef, Input, Renderer } from '@angular/core';
import { rendererTypeName } from '@angular/compiler';

@Directive({
  selector: '[appShowIf]'
})

/**
 * A directive to show/hide an element based on the value of a boolean expression
 * Every time the value of the boolean expression is set, the visibility is updated
 */
export class ShowIfDirective {

  //Allows the directive to be used like this [appShowIf] = booleanExpression
  @Input('appShowIf') set condition(boolExpr:boolean){
    console.log("changed property: " + boolExpr);
    this.renderer.setElementStyle(this.element.nativeElement,'visibility', boolExpr ? 'visible' : 'hidden');
  }
  constructor(public element:ElementRef, public renderer:Renderer) {
  }

}
