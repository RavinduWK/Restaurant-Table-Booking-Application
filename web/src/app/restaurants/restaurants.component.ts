import { NgModule, Component } from '@angular/core';
import { RestaurantService } from '../service/restaurant.service';
import { Restaurant } from '../models/restaurants.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-chefs',
  templateUrl: './restaurants.component.html',
  styleUrls: ['./restaurants.component.css'],
})
export class RestaurantsComponent {
  restaurants: Restaurant[] = [];
  filteredRestaurants: Restaurant[] = [];
  selectedRestaurant!: Restaurant | undefined;
  searchRestaurant = '';

  constructor(
    private restaurantService: RestaurantService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getRestaurants();
  }

  getRestaurants() {
    this.restaurantService.GetRestaurants().subscribe((s) => {
      this.restaurants = s;
      this.filteredRestaurants = s;
      console.log('Restaurants', this.restaurants);
    });
  }

  navigateToDetailsPage(restaurant: Restaurant): void {
    this.restaurantService.selectedRestaurant = restaurant;
    this.router.navigate(['restaurants/' + restaurant.id]);
  }
}
