import { Component,Inject } from '@angular/core';
import { Counterservice } from './counter.counterservice'; 


@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }

  
  constructor(private VideoService: Counterservice)
  {

  } 
  selectedfile : File =null;
  OnFileSelected(event)
  {
    this.selectedfile=<File> event.target.files[0];
  }
  onUpload(video)
  {
   this.VideoService.postfile(this.selectedfile);
  }

}
