# 1. Download the coronavirus package and load it
#install.packages("coronavirus")
require("dplyr")
#require(coronavirus)
#data("coronavirus")
#df <-coronavirus
#df <- read.csv("https://raw.githubusercontent.com/RamiKrispin/coronavirus-csv/master/coronavirus_dataset.csv")
setwd("~/Documents/HomeLabRepository/R/Statistics/Labs")
df <- read.csv("coronavirus_dataset.csv")
head(df) # check if data is downloaded

# Convert date to Date type
df['date'] <- as.Date(df$date,format='%Y-%m-%d')

# 2. Find the number of people currently infected in Ukraine.
# We consider infected people as people who are confirmed
infected_count <- function(c)
{
  subdf <- df %>% filter(Country.Region == c, type == "confirmed")
  ifelse(nrow(subdf) == 0, 0, sum(subdf['cases']))
}
print(infected_count("Ukraine"))

# 3. Select any three countries and calculate the current mortality rate (ratio of deaths to confirmed cases
cases_by_type <- function(dataset, t){
  subdf <- dataset %>% filter(type == t)
  ifelse(nrow(subdf) == 0, 0, sum(subdf['cases']))
}
ratio <-function(country){
  subdf <- df %>% filter(Country.Region == country)
  confirmed <- cases_by_type(subdf, "confirmed")
  death <- cases_by_type(subdf, "death")
  death / confirmed
}
print(ratio("Japan"))
print(ratio("Iran"))
print(ratio("China"))

# 4. Try to visualize the evolution of the number of people infected over time for any country
#install.packages("ggplot2") # Run only once
#install.packages("plotly") 
library("ggplot2")
library("plotly")
# Plot one country
evolution_vis <- function(country)
{
  subdf <- df %>% filter(Country.Region == country, type == "confirmed") %>%
    select(date, cases) %>%
    group_by(date) %>%
    summarise(total_cases = sum(cases)) # in case if data has several input during one day
  v <- subdf
  v['total_cum_cases'] <-cumsum(v['total_cases'])

  options(scipen=999)  # turn-off scientific notation like 1e+48
  theme_set(theme_bw())  # pre-set the bw theme.
  gg <- ggplot(v, aes(x=date, y=total_cum_cases))+
    geom_point(size=3, color="blue", alpha=0.7) +
    geom_smooth(method="auto") +
    labs(
      y="Cases", 
      x="Date", 
      title=paste0("Virus evolution in ", country), 
      caption = "Source: 2019 Novel Coronavirus") +
    scale_x_date(date_breaks = "months" , date_labels = "%b-%y")
  plot(gg)
}
# Plot several countries
evolution_countries_vis <-function(vec)
{
  res <- lapply(vec, function(country) {
    subdf <- df %>% filter(Country.Region == country, type == "confirmed") %>%
      select(Country.Region, date, cases) %>%
      group_by(Country.Region, date) %>%
      summarise(total_cases = sum(cases)) # in case if data has several input during one day
    v <- subdf
    v['total_cum_cases'] <- cumsum(v['total_cases'])
    v
    
  })

  merged_res <- res[[1]]
  for(i in range(2, length(res)))
  {
    merged_res <- rbind(merged_res, res[[i]])
  }
  merged_res
  
  options(scipen=999)  # turn-off scientific notation like 1e+48
  theme_set(theme_bw())  # pre-set the bw theme.
  gg <- ggplot(merged_res, aes(x=date, y=total_cum_cases, color=Country.Region))+
    geom_point(size=3, alpha=0.7) +
    geom_smooth(method="loess") +
    scale_color_discrete(name ="Country") +
    labs(
      y="Cases", 
      x="Date", 
      title="Virus evolution in countries", 
      caption = "Source: 2019 Novel Coronavirus")
  plot(gg)
}

country <- "China"
evolution_vis(country)

vec <- c("Japan", "Thailand")
evolution_countries_vis(vec)

# 5. Calculate the basic descriptive characteristics (average number of infected per day, the median of infected, 
# standard deviation of infected, skewness and kurtosis, and so on) of the number of infected over time for any 
# country and interpret these results.

#install.packages("TTR")
#install.packages("moments")
library("TTR") # use runMean, runMedian, runSD - run Standard Deviation
library("moments") # skewness and kurtosis
library("RColorBrewer")
cols <- brewer.pal(8, "Dark2") # Just to make our plots look pretty

# Write additional function to calculate average Skewness and average kurtosis
runSkewness <- function(x, n=10)
{
  x <- unlist(x)
  if( n < 1 || n > NROW(x) ) stop("Invalid 'n'")
  
  # Initialize result vector 
  result <- double(NROW(x))
  result[n:NROW(x)] <- sapply(1:(NROW(x)-n + 1), function(i) skewness(x[i:(i+n - 1)]))
  
  # Replace 1:(n-1) with NAs and prepend NAs from original data
  is.na(result) <- c(1:(n-1))
  #result <- c( rep( NA, NAs ), result )
  
  # Convert back to original class
  result
}
runKurtosis <- function(x, n=10)
{
  x <- unlist(x)
  if( n < 1 || n > NROW(x) ) stop("Invalid 'n'")
  
  # Initialize result vector 
  result <- double(NROW(x))
  result[n:NROW(x)] <- sapply(1:(NROW(x)-n + 1), function(i) kurtosis(x[i:(i+n - 1)]))
  
  # Replace 1:(n-1) with NAs and prepend NAs from original data
  is.na(result) <- c(1:(n-1))
  #result <- c( rep( NA, NAs ), result )
  
  # Convert back to original class
  result
}


country <- "China"

# Make data as a time series with equal intervals by adding 0 rows when no cases were registered
v <- df %>% filter(Country.Region == country, type == "confirmed") %>%
  select(date, cases) 
d <- v['date'][[1]]
rng <- range(d)
tbl <- tibble(
  date = seq(rng[1], rng[2], by="days"), 
  cases = 0
)
v <- rbind(v, tbl)
v <- v %>%
  group_by(date) %>%
  summarise(total_cases = sum(cases)) # in case if data has several input during one day
v['total_cum_cases'] <-cumsum(v['total_cases'])
v['runMean'] <- runMean(v['total_cum_cases'])
v['runMedian'] <- runMedian(v['total_cum_cases'])
v['runSD'] <- runSD(v['total_cum_cases'])
v['runSkewness'] <- runSkewness(v['total_cum_cases'])
v['runKurtosis'] <- runKurtosis(v['total_cum_cases'])

gg <- ggplot(v, aes(x=date)) +
  geom_smooth(aes(y = runMean, color=cols[2]), method = "auto", se = FALSE) +
  geom_smooth(aes(y = runMedian, color=cols[3]), method = "auto", se = FALSE) +
  geom_smooth(aes(y = runSD, color=cols[4]), method = "auto", se = FALSE) +
  geom_smooth(aes(y = runSkewness, color=cols[5]), method = "auto", se = FALSE) +
  geom_smooth(aes(y = runKurtosis, color=cols[6]), method = "auto", se = FALSE) +
  scale_color_identity(name = "",
                       breaks = cols[2:6],
                       labels = c("Moving Average", "Moving Median", "Moving Standard Deviation", "Moving Skewness", "Moving Kurtosis"),
                       guide = "legend") +
  labs(
    y="Cases", 
    x="Date", 
    title=paste0("Virus evolution in ", country),
    caption = "Source: 2019 Novel Coronavirus")
plot(gg)

# Interpretation (for China):
'
According to the last plot, we can say that we have a non-stationary process because our Moving Average and moving Standard Deviations
changes with time and we have upward trend.
Moving Skewness and Kurtosis are more or less stable.

'

# 6. Try to predict the number of people infected in the selected country for the next days based on the past growth rate. 
# Is the growth in the number of infected rather linear or exponential
evolution_vis(country)
x <- unlist(v['total_cum_cases'])
diff <- x[2:length(x)] - x[1:(length(x) - 1)]
growth_factor <- diff[2:length(diff)] / diff[1:(length(diff)-1)]
print(growth_factor)
# Create a model
fit <- arima(x, c(0, 2, 1))
# Make a prediction
pred <- predict(fit, n.ahead = 10)
p <- unlist(pred$pred)
# Show prediction with initial data
gg <- ggplot() +
  geom_point(aes(x=(1:length(x)), y = x), size=3, alpha=0.4, color=cols[1]) +
  geom_point(aes(x=(length(x) + 1) : (length(x) + length(p)), y = p), size=3, alpha=0.4, color=cols[2])
plot(gg)

# Interpretation (for Mainland China):
'
At first, our growth rate were bigger than 1 that means we have a exponential trend, but now it fluctuates near 1, so we are reaching 
an inflection point.
We have created ARIMA model and make a prediction for 10 steps
'
