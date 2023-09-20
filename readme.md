# Amex Points Calculator

This is a simple program that determines the value of points when booking a flight / hotel with an external partner or American Express rewards points. I wrote this to practice while learning C#, and the program has the following features:
- Calculate how much points are worth based on dollar price
- Compare value of Amex points to external points (such as Delta SkyMiles)
- Factor in earned points from the Amex Platinum card into the calculations
- Determine which booking option is the most cost effective based on a target value of points.

## Assumptions

In order for my janky math to work correctly, the program assumes three things:
1. You pay with an amex platinum card or other amex card that earns points on purchases
2. The points transition is 1:1 from amex to partner
3. No points double dipping (i.e. if you buy on AmexTravel you don't get amex points AND delta points)

*It's worth noting that Delta does actually give you points for booking on AmexTravel, but it makes the math messy so I don't include it. TLDR, you get a slightly better value for booking with dollars on AmexTravel than the program gives you credit for.*

## The Math:

Cost of buying from vender with dollars:
```
Cost of flight in dollars - [points you earn for buying it * points multiplier * points value]
```

Cost of buying from AmexTravel with points:
```
Cost of flight in dollars - [points you earn from buying it * points multiplier (5) * points value (0.01)]
```

Cost of buying from vender with points:
```
Cost of flight in points * target value of points (default 0.01)
```

Cost of buying from AmexTravel with points:
```
Cost of flight in points * value of points (0.01)
```