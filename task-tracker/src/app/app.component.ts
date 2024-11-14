import { RouterOutlet } from '@angular/router';
import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { TasksViewComponent } from './tasks-view/tasks-view.component';
import { MatCardModule } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NotificationService } from './notification.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    TasksViewComponent,
    MatCardModule,
    RouterLink,
    HttpClientModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'task-tracker';
  constructor(private notificationService: NotificationService) {
    this.notificationService.initWebSocket(); // in ngOnInit
  }
}
