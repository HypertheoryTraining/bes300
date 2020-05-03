import { Component, OnInit } from '@angular/core';
import { CurbsideHubService, OrderResponse, OrderRequest } from 'src/app/services/curbside.hub.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-curbside',
  templateUrl: './curbside.component.html',
  styleUrls: ['./curbside.component.scss']
})
export class CurbsideComponent implements OnInit {
  constructor(private curbsideHub: CurbsideHubService) { }

  order$: Observable<OrderResponse>;

  ngOnInit(): void {
    this.order$ = this.curbsideHub.getOrder();
  }

  placeOrder(forEl: HTMLInputElement, itemsEl: HTMLInputElement) {
    const order: OrderRequest = {
      for: forEl.value,
      items: itemsEl.value
    };
    this.curbsideHub.sendOrder(order);
    forEl.value = '';
    itemsEl.value = '';
  }

}
