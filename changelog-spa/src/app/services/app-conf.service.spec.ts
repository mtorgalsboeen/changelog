import { TestBed } from '@angular/core/testing';

import { AppConfService } from './app-conf.service';

describe('AppConfService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AppConfService = TestBed.get(AppConfService);
    expect(service).toBeTruthy();
  });
});
