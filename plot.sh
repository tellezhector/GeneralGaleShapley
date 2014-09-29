set terminal postscript eps enhanced color font 'Helvetica,10'
set output 'results.ps'
set xlabel 'Case size'
plot 'plottable.txt' using 1:2 with lines title "Classic Galey-Shapley", '' using 3:4 with lines title "n", '' using 7:8 with lines title "n log(n)", '' using 9:10 with lines title "2n log(n)"
