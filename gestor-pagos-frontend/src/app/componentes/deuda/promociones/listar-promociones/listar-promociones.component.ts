import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PromocionService } from '../../../../servicios/Promocion.service';
import { ModalComponent } from '../../../modal/modal.component';

@Component({
  selector: 'app-listar-promociones',
  standalone: true,
  imports: [CommonModule, FormsModule, ModalComponent],
  templateUrl: './listar-promociones.component.html',
  styleUrls: ['./listar-promociones.component.css']
})
export class ListarPromocionesComponent implements OnInit {
  filtro = {
    tipoServicio: 'ambos'
  };

  promociones: any[] = [];

  // modalOpen = false;
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
    const { tipoServicio } = this.filtro;
    const tipo = tipoServicio === 'ambos' ? '' : tipoServicio;

    this.service.listarPromociones('', '', undefined, tipo)
      .subscribe((data) => {
        this.promociones = data.filter(promo => {
          if (tipoServicio === 'ambos') return true;
          return promo.tipoPromocion?.toLowerCase() === tipoServicio.toLowerCase();
        });
      });
  }

  cambiarEstado(id: number, estadoActual: boolean) {
    const nuevoEstado = !estadoActual;
    this.service.cambiarEstadoPromocion(id, nuevoEstado).subscribe({
      next: () => this.listar(),
      error: (err) => {
        console.error('Error al cambiar estado:', err);
        alert('Ocurrió un error al cambiar el estado.');
      }
    });
  }

  // toggleModal() {
  //   this.modalOpen = true;
  // }
  openModal() {

    this.isModalOpen = true;
    this.active = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  // handleConfirm() {
  //   alert(`Enviado!!`);
  //   this.closeModal();
  // }
}

