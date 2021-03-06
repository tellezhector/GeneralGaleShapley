[data, l] = read_data();
means = mean(data);
i  = 3*(0:(l-1));
n = means(i+1);
iterations = means(i+2);
variances = var(data, 1)(i+2);
deviations = sqrt(variances);
nlogn = n .* log(n);
y = [n; iterations; deviations; n; n; n; n .* n; n; nlogn; n; 2 .* (nlogn)];
dlmwrite("plottable.txt", y', " ");
