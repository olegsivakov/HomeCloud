import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProgressService } from '../../../services/shared/progress/progress.service';
import { ISubscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-progress',
  templateUrl: './progress.component.html',
  styleUrls: ['./progress.component.css']
})
export class ProgressComponent implements OnInit, OnDestroy {

  private progressChangedSubscription: ISubscription = null;
  private isInProgress: boolean = false;

  constructor(private progressService: ProgressService) { }

  ngOnInit() {
    this.progressChangedSubscription = this.progressService.progressChanged$.subscribe(isVisible => {
      this.isInProgress = isVisible;
    });
  }

  ngOnDestroy(): void {
    if (this.progressChangedSubscription) {
      this.progressChangedSubscription.unsubscribe();
      this.progressChangedSubscription = null;
    }
  }
}
