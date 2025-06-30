import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title(title: any) {
    throw new Error('Method not implemented.');
  }
  constructor(private router: Router) {}

  irAPromociones() {
    this.router.navigate(['/promociones']);
  }

  irACrear() {
    this.router.navigate(['/crear']);
  }

  irADeuda() {
  this.router.navigate(['/deuda']);
}
}

