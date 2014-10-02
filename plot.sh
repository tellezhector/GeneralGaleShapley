set terminal postscript eps enhanced color font 'Helvetica,10'
set output 'results.ps'
set xlabel 'Case size'
plot    'plottable.txt' using 1:2:3 with yerrorbars lt 1 lc rgb "#FF0000" title "error", \
        '' using 1:2 with lines lt rgb "#000000" title "Classic Galey-Shapley", \
        '' using 4:5 with lines title "n", \
        '' using 8:9 with lines lt rgb "#0000FF" title "n log(n)", \
        '' using 10:11 with lines title "2n log(n)"

