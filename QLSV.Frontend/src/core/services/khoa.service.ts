import { Injectable } from '@angular/core';
import { ApiBaseService } from './api-base.service';
import { Khoa } from '../models/khoa.model';

@Injectable({
  providedIn: 'root',
})

export class KhoaService extends ApiBaseService<Khoa> {
  protected override resource = 'Khoa';
}
