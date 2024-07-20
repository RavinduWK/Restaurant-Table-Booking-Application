import { Component } from '@angular/core';
import { Restaurant } from '../models/restaurants.model';
import { RestaurantService } from '../service/restaurant.service';
import { Router } from '@angular/router';

@Component({
  selector: 'restaurant-details',
  templateUrl: './restaurant-details.component.html',
  styleUrls: ['./restaurant-details.component.css'],
})
export class RestaurantDetailsComponent {
  selectedRestaurant!: Restaurant | undefined;

  constructor(
    private restaurantService: RestaurantService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.selectedRestaurant = this.restaurantService.selectedRestaurant;
  }
}
