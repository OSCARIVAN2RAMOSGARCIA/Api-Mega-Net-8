import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalComponent } from '../../modal/modal.component';
import { PromocionService } from '../../../servicios/Promocion.service';

@Component({
  selector: 'app-listar-promociones',
  standalone: true,
  imports: [CommonModule, FormsModule, ModalComponent],
  templateUrl: './listar-promociones.component.html',
})
export class ListarPromocionesComponent implements OnInit {
  filtro = {
    tipoServicio: 'ambos'
  };
  ciudad = '';
  colonia = '';
  promociones: any[] = [];
  loading = false;
  changingStates: {[id: number]: boolean} = {}; // Para rastrear estados cambiantes

  isModalOpen = false;
  active = false;
  modalTitle = '';
  modalMessage = '';
  modalConfirmText = '';

  constructor(private service: PromocionService) { }

  ngOnInit(): void {
    this.listar();
  }

  listar() {
    this.loading = true;
    const tipo = this.filtro.tipoServicio === 'ambos' ? '' : this.filtro.tipoServicio;
    
    this.service.listarPromociones(this.ciudad, this.colonia, undefined, tipo)
      .subscribe({
        next: (data) => {
          this.promociones = data;
          this.loading = false;
        },
        error: (err) => {
          console.error('Error al listar promociones:', err);
          this.loading = false;
          alert('Ocurrió un error al cargar las promociones');
        }
      });
  }

  cambiarEstado(id: number, estadoActual: boolean) {
    const nuevoEstado = !estadoActual;
    this.changingStates[id] = true; // Marcar que esta promoción está cambiando
    
    this.service.cambiarEstadoPromocion(id, nuevoEstado).subscribe({
      next: () => {
        // Actualizar el estado localmente sin recargar toda la lista
        const promoIndex = this.promociones.findIndex(p => p.idPromocion === id);
        if (promoIndex !== -1) {
          this.promociones[promoIndex].activa = nuevoEstado;
        }
        this.changingStates[id] = false;
      },
      error: (err) => {
        console.error('Error al cambiar estado:', err);
        this.changingStates[id] = false;
        alert('Ocurrió un error al cambiar el estado.');
      }
    });
  }

  openModal() {
    this.isModalOpen = true;
    this.active = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }
}