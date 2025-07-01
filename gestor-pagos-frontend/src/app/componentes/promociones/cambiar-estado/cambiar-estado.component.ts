import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PromocionService } from '../../../servicios/Promocion.service';

@Component({
  selector: 'app-cambiar-estado',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cambiar-estado.component.html',
  styleUrls: ['./cambiar-estado.component.css']
})
export class CambiarEstadoComponent {
  id: number = 0;
  activa: boolean = true;

  constructor(private service: PromocionService) { }

}

