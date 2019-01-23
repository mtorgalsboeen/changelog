import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-common-form',
  templateUrl: './common-form.component.html',
  host: {'class':'formContainer'}
})

/*Provides a common container for containing forms*/
export class CommonFormComponent implements OnInit {
 
  @Input() header:string;
  constructor() { }

  ngOnInit() {
  }

}
