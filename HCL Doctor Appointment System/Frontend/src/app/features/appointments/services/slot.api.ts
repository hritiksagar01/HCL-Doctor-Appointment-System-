import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Slot } from '../models/slot.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SlotApiService {
  private readonly http = inject(HttpClient);

  getSlotsByDoctor(doctorId: number): Observable<Slot[]> {
    return this.http.get<Slot[]>(`${environment.apiUrl}/slots/doctor/${doctorId}`).pipe(
      map(slots =>
        slots.sort((a, b) => {
          const av = `${a.slotDate}T${a.startTime}`;
          const bv = `${b.slotDate}T${b.startTime}`;
          return av.localeCompare(bv);
        })
      )
    );
  }
}
