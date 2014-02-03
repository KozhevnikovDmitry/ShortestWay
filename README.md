ShortestWay
===========

Test task#2 implementation for vacancy http://www.interesnee.ru/join-our-team/25/dotnet-developer

Application provides command line interface and finds shortest way in graph using Dijkstra algorythm. I have distinguished three main responsibilities: loading data from external source, implementing of common road net rules, finding the shortest way from start to finish with particular algorythm. According to this I have implemented GraphProvider, common graph model(Graph, Node, Link), dijkstra graph model(DijkstraGraph. DijkstraNode) and Dijkstra class, that implements algorythm itself. To my mind, this solution is a little bit overdesigned, but if you want OOP, you receive it. 
