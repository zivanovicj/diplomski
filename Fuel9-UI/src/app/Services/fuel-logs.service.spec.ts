import { TestBed } from '@angular/core/testing';

import { FuelLogsService } from './fuel-logs.service';

describe('FuelLogsService', () => {
  let service: FuelLogsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FuelLogsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
